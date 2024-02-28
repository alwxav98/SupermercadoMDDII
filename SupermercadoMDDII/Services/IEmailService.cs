using SupermercadoMDDII.Implementacion;

namespace SupermercadoMDDII.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailDTO request);
    }
}
