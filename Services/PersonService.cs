using AutoMapper;
using Azure.Core;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.Extensions.Exceptions;
using HealthCareApplication.Resource.Persons;

namespace HealthCareApplication.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PersonViewModel> GetPerson(string personId)
    {
        var person = await _personRepository.GetAsync(personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);
        return _mapper.Map<PersonViewModel>(person);
    }
    public async Task<PersonInfoViewModel> GetPersonInfo(string personId)
    {
        var person = await _personRepository.GetPersonInfoAsync(personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);
        return _mapper.Map<PersonInfoViewModel>(person);
    }

        public async Task<bool> CreatePerson(CreatePersonViewModel viewModel)
    {
        var person = _mapper.Map<CreatePersonViewModel, Person>(viewModel);
        await _personRepository.Add(person);

        return await _unitOfWork.CompleteAsync();
    }

    public async Task<bool> UpdatePerson(string personId, UpdatePersonViewModel viewModel)
    {
        var person = await _personRepository.GetAsync(personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);
        var address = _mapper.Map<Address>(viewModel.Address);

        person.Update(viewModel.Name, viewModel.Age, viewModel.PersonType, address, viewModel.Weight, viewModel.Height, viewModel.PhoneNumber, viewModel.ImagePath);
        _personRepository.Update(person);
        
        return await _unitOfWork.CompleteAsync();
    }

    public async Task<bool> DeletePerson(string personId)
    {
        await _personRepository.DeleteAsync(personId);
        return await _unitOfWork.CompleteAsync();
    }
}
