using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Enums;
using ThreeDeeApplication.Models;

namespace ThreeDeeFrontend.ViewModels;

public interface IFilesGridViewModel
{
    List<FileModel> FilteredFiles { get; }
    Task Init(string userId);
    EventCallback FilesChanged { get; set; }
    string SelectedSearchValue { get; set; }
    Filetype FileAccessStatus { get; }
    Task<IEnumerable<string>> UpdateFilteredFiles(string searchValue);
    Task UpdateFilteredFiles(Filetype newStatus);
}