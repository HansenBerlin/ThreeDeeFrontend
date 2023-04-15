using Microsoft.AspNetCore.Components;
using MudBlazor;
using ThreeDeeApplication.Enums;
using ThreeDeeApplication.Models;
using ThreeDeeFrontend.Services;
using ThreeDeeInfrastructure.Repositories;
using ThreeDeeInfrastructure.RequestModels;
using ThreeDeeInfrastructure.ResponseModels;


namespace ThreeDeeFrontend.ViewModels;

public class FilesGridViewModel : IFilesGridViewModel
{
    public EventCallback FilesChanged { get; set; }
    public string SelectedSearchValue { get; set; } = "";
    public List<FileModel> FilteredFiles { get; private set; } = new();
    public Filetype FileAccessStatus { get; private set; }
    
    private List<FileModel> _files = new();
    private readonly IRepository<FileModel, FileRequestModel> _fileRepository;
    private string _userId = "";
    
    public FilesGridViewModel(IRepository<FileModel, FileRequestModel> fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task UpdateFilteredFiles(Filetype newStatus)
    {
        FileAccessStatus = newStatus;
        if (newStatus == Filetype.Public)
        {
            FilteredFiles = new List<FileModel>(_files);
        }
        else
        {
            FilteredFiles = new List<FileModel>(_files)
                .Where(x => x.Permission == "owner")
                .ToList();
        }
        
        await FilesChanged.InvokeAsync();
    }
    
    public async Task Init(string userId)
    {
        _userId = userId;
        var files = await _fileRepository.GetAll($"user/{_userId}");
        _files = new List<FileModel>(files);
        FilteredFiles = new List<FileModel>(_files);
        await FilesChanged.InvokeAsync();
    }
    
    public async Task<IEnumerable<string>> UpdateFilteredFiles(string searchValue)
    {
        IEnumerable<string> filtered;
        if (string.IsNullOrEmpty(searchValue))
        {
            FilteredFiles = new List<FileModel>(_files);
            filtered = new List<string>();
        }
        else
        {
            FilteredFiles = new List<FileModel>(_files)
                .Where(x => x.FullName
                    .Contains(searchValue, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
            filtered = FilteredFiles.Select(x => x.FullName);
        }
        await FilesChanged.InvokeAsync();
        return filtered;
    }
}