namespace MobileDev_Projekt.Services
{
  public interface IMessage
  {
    void LongAlert(string message);
    void ShortAlert(string message);
  }
}