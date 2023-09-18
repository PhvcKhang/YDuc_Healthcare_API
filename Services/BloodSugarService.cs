using AutoMapper;
using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.Extensions.Exceptions;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.BloodPressure;
using Python.Runtime;
using HealthCareApplication.Domains.Persistence.Repositories;

namespace HealthCareApplication.Services;

public class BloodSugarService : IBloodSugarService
{
    private readonly IBloodSugarRepository _bloodSugarRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BloodSugarService(IBloodSugarRepository bloodSugarRepository, IPersonRepository personRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _bloodSugarRepository = bloodSugarRepository;
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BloodSugarViewModel> GetNewestAsync()
    {
        var bloodPressure = await _bloodSugarRepository.GetNewestAsync();
        return _mapper.Map<BloodSugarViewModel>(bloodPressure);
    }

    public async Task<List<BloodSugarViewModel>> GetBloodSugars(string personId, TimeQuery timeQuery)
    {
        var bloodSugars = await _bloodSugarRepository.GetListByTimeQueryAsync(personId, timeQuery);
        return _mapper.Map<List<BloodSugarViewModel>>(bloodSugars);
    }

    public async Task<BloodSugar> CreateBloodSugar(string personId, CreateBloodSugarViewModel viewModel)
    {
        var person = await _personRepository.GetAsync(personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);
        var bloodSugar = new BloodSugar(
            viewModel.Value,
            viewModel.ImageLink,
            DateTime.UtcNow.AddHours(7),
            person);

        var entity = await _bloodSugarRepository.Add(bloodSugar);
        await _unitOfWork.CompleteAsync();
        return entity;
    }
}
