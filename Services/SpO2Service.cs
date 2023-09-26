using AutoMapper;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Persistence.Repositories;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.Extensions.Exceptions;
using HealthCareApplication.Resource.SpO2;

namespace HealthCareApplication.Services
{
    public class SpO2Service : ISpO2Service
    {
        private readonly ISpO2Repository _spO2Repository;
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SpO2Service(ISpO2Repository spO2Repository, IPersonRepository personRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _spO2Repository = spO2Repository;
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SpO2> CreateSpO2(string personId, CreateSpO2ViewModel viewModel)
        {
            var person = await _personRepository.GetAsync(personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);
            var spO2 = new SpO2(
            viewModel.Value,
            viewModel.ImageLink,
            DateTime.UtcNow.AddHours(7),
            person);
            var entity = await _spO2Repository.Add(spO2);
            await _unitOfWork.CompleteAsync();
            return entity;
        }

        public async Task<List<SpO2ViewModel>> GetAll(string personId, TimeQuery timeQuery)
        {
            var spO2s = await _spO2Repository.GetAllAsync(personId, timeQuery);
            var viewModel = _mapper.Map<List<SpO2>,List<SpO2ViewModel>>(spO2s);
            return viewModel;
        }
    }
}
