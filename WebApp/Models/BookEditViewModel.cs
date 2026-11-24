using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

public class BookEditViewModel : BookFormViewModel
{
    [HiddenInput]
    public int Id { get; set; }
}