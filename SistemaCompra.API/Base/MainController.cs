using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.API.Base
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                if (result is null)
                {
                    return Ok();
                }

                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected ObjectResult CustomResponseCreated(object result = null)
        {
            if (OperacaoValida())
            {
                return Created(string.Empty, result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponseCreated(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponseCreated();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }
    }
}
