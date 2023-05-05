using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ThreeDeeApplication.Enums;
using ThreeDeeApplication.Models;
using ThreeDeeFrontend.ViewModels;
using ThreeDeeInfrastructure.Repositories;

namespace ThreeDeeFrontend.Components;

public partial class SearchBar
{
    [Parameter] 
    public string SelectedSearchValue { get; set; }

    [Parameter]
    public Func<string, Task<IEnumerable<string>>> Search { get; set; }
}