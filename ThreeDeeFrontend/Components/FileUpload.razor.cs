using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ThreeDeeApplication.Models;
using ThreeDeeFrontend.Services;
using ThreeDeeInfrastructure.Repositories;
using ThreeDeeInfrastructure.RequestModels;

namespace ThreeDeeFrontend.Components;

public partial class FileUpload
{
    [Parameter]
    public EventCallback<int> UploadStateHasChanged { get; set; }
    
    [Inject] 
    public IRepository<FileModel, FileRequestModel> FileRepository { get; set; }
    
    [Inject] 
    public AuthenticationValidator AuthenticationValidator { get; set; }

    //IList<IBrowserFile> _files = new List<IBrowserFile>();
    private int _maxAllowedSize = 1024 * 1024 * 1024;

    private async Task UploadFiles(IBrowserFile file)
    {
        //_files.Clear();
        var path = Path.Combine(Environment.WebRootPath,
            "stlfiles", file.Name);
        if (File.Exists(path))
        {
            Snackbar.Add($"File with name {file.Name} alrady exists. Upload cancelled. Please rename your file first and retry.", Severity.Warning);
            return;
        }
        if (file.Name[^4..] != ".stl")
        {
            Snackbar.Add($"File must be an stl file. Upload cancelled.", Severity.Error);
            return;
        }
        await using FileStream writeStream = new(path, FileMode.Create);
        await using var readStream = file.OpenReadStream(_maxAllowedSize);
        var buffer = new byte[1024 * 10];
        long totalSize = readStream.Length;
        long totalRead = 0;
        long percentCache = 0;
        try
        {
            _isUploading = true;
            bool isViewUpdateRequested = false;
            var sw = Stopwatch.StartNew();
            int bytesRead;
            while ((bytesRead = await readStream.ReadAsync(buffer)) != 0)
            {
                if (_forceStop)
                {
                    break;
                }
                await writeStream.WriteAsync(buffer, 0, bytesRead);
                totalRead += bytesRead;

                if (isViewUpdateRequested == false)
                {
                    if (sw.ElapsedMilliseconds > 1000)
                    {
                        sw.Stop();
                        isViewUpdateRequested = true;
                    }
                }
                else
                {
                    int percentComplete = (int)((double)totalRead / totalSize * 100);
                    if (percentComplete % 1 == 0 && percentComplete != percentCache)
                    {
                        percentCache = percentComplete;
                        //await UploadStateHasChanged.InvokeAsync(percentComplete);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw;
            Snackbar.Add($"Upload of {file.Name} failed.", Severity.Error);
        }
        
        if (_forceStop == false)
        {
            //_files.Add(file);
            string userId = await AuthenticationValidator.GetUserId();
            string fileName = file.Name[..^4];
            var fileRequest = new FileRequestModel()
            {
                Fullname = fileName,
                OwnerUserId = userId,
                Sizebytes = file.Size
            };
            var response = await FileRepository.Insert(fileRequest);
            if (response.IsResponseSuccess)
            {
                Snackbar.Add($"{file.Name} uploaded successfully.", Severity.Success);
            }
            else
            {
                Snackbar.Add($"Upload of {file.Name} failed.", Severity.Error);
                await readStream.DisposeAsync();
                await writeStream.DisposeAsync();
                File.Delete(path);
            }
        }
        else
        {
            //writeStream.Close();
            Snackbar.Add($"Upload of {file.Name} cancelled.", Severity.Info);
            await readStream.DisposeAsync();
            await writeStream.DisposeAsync();
            File.Delete(path);
            //_forceStop = false;
        }
        _isUploading = false;
        _forceStop = false;
        await UploadStateHasChanged.InvokeAsync(0);
    }

    private bool _forceStop;
    private bool _isUploading;

    private void Callback()
    {
        _forceStop = true;
    }

    protected override void OnInitialized()
    {
        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;

    }
}