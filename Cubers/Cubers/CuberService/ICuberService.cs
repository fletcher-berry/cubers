using Cubers.Models;

namespace Cubers
{
    public interface ICuberService
    {
        void AddCuber(Cuber cuber);
        void AddSolve(PostedTime solve);
        void DeleteCuber(int cuberId);
        Cuber GetCuber(int id);
        CuberSummary GetCuberSummary();
        void SaveCuber(Cuber cuber);
    }
}