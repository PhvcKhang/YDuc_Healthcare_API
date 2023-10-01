using Azure.Core;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.OneSignal;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Doctors;
using HealthCareApplication.Resource.Persons.Patients;
using HealthCareApplication.Resource.Persons.Relatives;
using MesMicroservice.Api.Application.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Diagnostics;

namespace HealthCareApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : Controller
{
    #region Properties & Constructor
    private readonly IPersonService _personService;

    public PersonsController(IPersonService personService)
    {
        _personService = personService;

    }
    #endregion Properties & Constructor

    #region Admin
    [HttpPost]
    [Route("CreateDoctorAccount")]
    public async Task<IActionResult> CreateDoctorAccount([FromBody] DoctorRegistrationViewModel registrationModel)
    {
        try
        {
            var result = await _personService.CreateDoctorAccount(registrationModel);
            return new OkObjectResult($"DoctorId: {result}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    #endregion Admin
    
    #region Patient
    [HttpGet]
    [Route("PatientProfile/{patientId}")]
    public async Task<PatientProfileViewModel> GetPatientInfo([FromRoute] string patientId)
    {
        return await _personService.GetPatientInfo(patientId);
    }

    [HttpGet]
    [Route("AllPatients")]
    public async Task<List<PatientsViewModel>> GetAllPatients()
    {
        return await _personService.GetAllPatients();
    }

    [HttpPost]
    [Route("{patientId}/AddRelative")]
    public async Task<IActionResult> AddRelative([FromBody] AddNewRelativeViewModel addNewRelativeViewModel, [FromRoute] string patientId)
    {
        try
        {
            var result = await _personService.AddRelative(addNewRelativeViewModel, patientId);
            return new OkObjectResult($"RelativetId: {result}");
            
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    #endregion Patient

    #region Doctor
    [HttpGet]
    [Route("DoctorProfile/{doctorId}")]
    [Authorize]
    public async Task<DoctorIProfileViewModel> GetDoctorInfo([FromRoute] string doctorId)
    {
        return await _personService.GetDoctorInfo(doctorId);
    }

    [HttpGet]
    [Route("AllDoctors")]
    public async Task<List<DoctorsViewModel>?> GetAllDoctors()
    {
        return await _personService.GetAllDoctors();
    }

    [HttpPost]
    [Route("{doctorId}/AddNewPatient")]
    public async Task<IActionResult> AddNewPatient([FromBody] AddNewPatientViewModel addNewPatientViewModel, string doctorId)
    {
        try
        {
            var result = await _personService.AddNewPatient(addNewPatientViewModel, doctorId);
            return new OkObjectResult($"PatientId: {result}");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpDelete]
    [Route("DeletePatient/{patientId}")]
    public async Task<IActionResult> DeletePatientById([FromRoute] string patientId)
    {
        try
        {
            var result = await _personService.DeletePatientById(patientId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion Doctor

    #region Relatives
    [HttpGet]
    [Route("AllRelatives")]
    public async Task<List<RelativesViewModel>> GetAllRelatives()
    {
        return await _personService.GetAllRelatives();
    }

    [HttpGet]
    [Route("RelativeProfile/{relativeId}")]
    public async Task<RelativeProfileViewModel> GetRelativeById( [FromRoute]string relativeId)
    {
        return await _personService.GetRelativeById(relativeId);
    }
    #endregion Relatives

}
