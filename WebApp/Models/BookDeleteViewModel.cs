using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

public class BookDeleteViewModel
{
    [HiddenInput]
    public int Id { get; set; }
}