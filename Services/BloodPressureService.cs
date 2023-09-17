using AutoMapper;
using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.Extensions.Exceptions;
using HealthCareApplication.Resource.BloodPressure;
using Python.Runtime;

namespace HealthCareApplication.Services;

public class BloodPressureService : IBloodPressureService
{
    private readonly IBloodPressureRepository _bloodPressureRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BloodPressureService(IBloodPressureRepository bloodPressureRepository, IPersonRepository personRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _bloodPressureRepository = bloodPressureRepository;
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BloodPressureViewModel> GetNewestAsync()
    {
        var bloodPressure = await _bloodPressureRepository.GetNewestAsync();
        return _mapper.Map<BloodPressureViewModel>(bloodPressure);
    }

    public async Task<List<BloodPressureViewModel>> GetBloodPressures(string personId, TimeQuery timeQuery)
    {
        var bloodPressures = await _bloodPressureRepository.GetListByTimeQueryAsync(personId, timeQuery);
        return _mapper.Map<List<BloodPressureViewModel>>(bloodPressures);
    }

    public async Task<bool> CreateBloodPressure(string personId, CreateBloodPressureViewModel viewModel)
    {
        var person = await _personRepository.GetAsync(personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);
        var bloodPressure = new BloodPressure(
            viewModel.Systolic,
            viewModel.PulseRate,
            viewModel.ImageLink,
            DateTime.UtcNow.AddHours(7),
            person);

        await _bloodPressureRepository.Add(bloodPressure);
        return await _unitOfWork.CompleteAsync();
    }
}
