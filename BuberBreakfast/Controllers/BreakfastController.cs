using AutoMapper;
using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

public class BreakfastController : MainController
{
    private readonly IBreakfastService _breakfastService;
    private readonly IMapper _mapper;

    public BreakfastController(IBreakfastService breakfastService, IMapper mapper)
    {
        _breakfastService = breakfastService;
        _mapper = mapper;
    }   

    [HttpPost("")]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var breakfast = new Breakfast(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet);

        ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);

        return createBreakfastResult.Match(
            created => CreateAtGetBreakfast(breakfast),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

        return getBreakfastResult.Match(
            breakfast => Ok(_mapper.Map<BreakfastResponse>(breakfast)),
            errors => Problem(errors)
        );
    }
    
    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        var breakfast = new Breakfast(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet);

        ErrorOr<UpsertedBreakfastResult> upsertResult = _breakfastService.UpsertBreakfast(breakfast);

        return upsertResult.Match(
            upsert => upsert.isNewlyCreated ? CreateAtGetBreakfast(breakfast) : NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deletedResult = _breakfastService.DeleteBreakfast(id);

        return deletedResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }

    private CreatedAtActionResult CreateAtGetBreakfast(Breakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: _mapper.Map<BreakfastResponse>(breakfast));
    }

}
