namespace MediaWorld.Domain.Interfaces
{
   public interface IControl
   {
      bool Play();
      bool Pause();
      bool Stop();
      bool Rewind();
      bool Forward();
   }
}