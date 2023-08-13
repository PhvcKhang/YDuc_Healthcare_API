using AutoMapper;
using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.Extensions.Exceptions;
using HealthCareApplication.Resource.BodyTemperature;
using HealthCareApplication.Resource.BloodSugar;
using Python.Runtime;
using HealthCareApplication.Domains.Persistence.Repositories;

namespace HealthCareApplication.Services;

public class BodyTemperatureService : IBodyTemperatureService
{
    private readonly IBodyTemperatureRepository _bodyTemperatureRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BodyTemperatureService(IBodyTemperatureRepository bodyTemperatureRepository, IPersonRepository personRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _bodyTemperatureRepository = bodyTemperatureRepository;
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BodyTemperatureViewModel> GetNewestAsync()
    {
        var bloodPressure = await _bodyTemperatureRepository.GetNewestAsync();
        return _mapper.Map<BodyTemperatureViewModel>(bloodPressure);
    }

    public async Task<List<BodyTemperatureViewModel>> GetBodyTemperatures(string personId, TimeQuery timeQuery)
    {
        var bodyTemperatures = await _bodyTemperatureRepository.GetListByTimeQueryAsync(personId, timeQuery);
        return _mapper.Map<List<BodyTemperatureViewModel>>(bodyTemperatures);
    }

    public async Task<bool> CreateBodyTemperature(string personId, CreateBodyTemperatureViewModel viewModel)
    {
        var person = await _personRepository.GetAsync(personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);
        var bodyTemperature = new BodyTemperature(
            viewModel.Value,
            viewModel.ImageLink,
            DateTime.UtcNow.AddHours(7),
            person);

        await _bodyTemperatureRepository.Add(bodyTemperature);
        return await _unitOfWork.CompleteAsync();
    }
}
