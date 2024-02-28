using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermercadoMDDII.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace SupermercadoMDDII.Implementacion
{
    public class EmailDTO
    {

        public string Para { get; set; } = string.Empty;
        public string Asunto { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
       

    }
}
