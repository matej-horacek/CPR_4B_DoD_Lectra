using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController, Route("subjects")]
    public class SubjectController(SubjectService subjectService) : Controller
    {
        [HttpGet]
        public async Task<IEnumerable<SubjectDTO>> GetAll(CancellationToken ct) =>
            (await subjectService.GetSubjectsAsync(ct)).Select(s => s.Dto());

        [HttpGet("{id}")]
        public async Task<SubjectDTO> Get(int id, CancellationToken ct) =>
            (await subjectService.GetSubjectByIdAsync(id, ct)).Dto();
        
        [HttpPost]
        public async Task<IActionResult> Create(string name, CancellationToken ct)
        {
            var createdSubject = await subjectService.CreateSubjectAsync(name, ct);
            return CreatedAtAction(nameof(Get), new { id = createdSubject.Id }, createdSubject);
        }
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RenameSubjectRequest request, CancellationToken ct)
        {
            var updatedSubject = await subjectService.RenameSubjectAsync(id, request.NewName, ct);
            return Ok(updatedSubject.Dto());
        }
    }
}
