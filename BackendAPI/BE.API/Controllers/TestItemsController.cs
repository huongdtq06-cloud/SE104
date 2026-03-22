using BackendAPI.BE.DAL.Interfaces;
using BackendAPI.BE.API.DTO;
using Microsoft.AspNetCore.Mvc;
using TestItemModel = BackendAPI.BE.DAL.Entities.TestItem;

using AutoMapper;

namespace BackendAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestItemsController : ControllerBase
{
    private readonly IRepository<TestItemModel> _repository;
    private readonly IMapper _mapper;

    public TestItemsController(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TestItemsController(IRepository<TestItemModel> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<TestItemDTO>> GetAll()
    {
        var items = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TestItemDTO>>(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TestItemDTO>> GetById(int id)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item == null)
            return NotFound();

        return _mapper.Map<TestItemDTO>(item);
    }

    [HttpPost]
    public async Task<ActionResult<TestItemDTO>> Create(TestItemDTO model)
    {
        var item = await _repository.AddAsync(_mapper.Map<TestItemModel>(model));
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<TestItemDTO>(item));
    }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> Update(int id, TestItemDTO model)
    // {
    //     if (id != model.Id)
    //         return BadRequest();

    //     await _repository.UpdateAsync(_mapper.Map<TestItemModel>(model));
    //     return NoContent();
    // }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }

    // private static TestItemDTO MapToDto(TestItemModel model) => new()
    // {
    //     Name = model.Name
    // };

    // private static TestItemModel MapToModel(TestItemDTO dto) => new()
    // {
    //     Id = dto.Id,
    //     Name = dto.Name
    // };
}
