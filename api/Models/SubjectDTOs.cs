namespace api.Models;

public record SubjectDTO
(
    int Id,
    string Name
);

public record RenameSubjectRequest(
    string NewName
);