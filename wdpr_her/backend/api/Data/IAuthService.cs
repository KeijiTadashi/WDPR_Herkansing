using api.DataTemplate;
using Microsoft.AspNetCore.Mvc;

namespace api;

public interface IAuthService
{
    public Task<ActionResult> Registreer(DTORegistreer dto);
    public Task<ActionResult> Login(DTOLogin dto);
}