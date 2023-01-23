using library_management_system.model;

namespace library_management_system.io.file;

public interface IFileManager
{
    Task<Library> ImportData();
    void ExportData(Library library);
}