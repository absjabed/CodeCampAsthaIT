using CodeCampWebapiClass.Models;

namespace CodeCampWebapiClass.Interfaces;
public interface ICodeCampStudentService
{
    public bool Delete(int id);
    public bool Update(int id, CodeCamperModel updatedItem);
    public void Add(CodeCamperModel newItem);
    public CodeCamperModel GetById(int id);
    public IEnumerable<CodeCamperModel> GetAll();
}
