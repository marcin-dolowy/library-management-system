using library_management_system.model;

namespace library_management_system.io.file;

public interface FileManager
{
    Library ImportData();
    void ExportData(Library library);
}