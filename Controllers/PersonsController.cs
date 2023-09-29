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

    private readonly NotificationHelper _pushNotification;

    public PersonsController(IPersonService personService)
    {
        _personService = personService;
        _pushNotification = new NotificationHelper();

    }
    #endregion Properties & Constructor

    #region Person
    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePersonViewModel person)
    {
        try
        {
            var result = await _personService.CreatePerson(person);
            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new ErrorMessage(ex);
            return BadRequest(errorMessage);
        }
    }

    [HttpPut]
    [Route("{personId}")]
    public async Task<IActionResult> UpdatePerson([FromRoute] string personId, [FromBody] UpdatePersonViewModel person)
    {
        try
        {
            var result = await _personService.UpdatePerson(personId, person);
            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new ErrorMessage(ex);
            return BadRequest(errorMessage);
        }
    }

    [HttpGet]
    [Route("{personId}")]
    public async Task<Person> GetPerson([FromRoute] string personId)
    {
        var result = await _personService.GetPerson(personId);
        return result;
    }

    [HttpDelete]
    [Route("{personId}")]
    public async Task<IActionResult> DeletePerson([FromRoute] string personId)
    {
        try
        {
            var result = await _personService.DeletePerson(personId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new ErrorMessage(ex);
            return BadRequest(errorMessage);
        }
    }

        #endregion Person
    
    #region Patient
    [HttpGet]
    [Route("PatientInfo/{patientId}")]
    public async Task<PatientInfoViewModel> GetPatientInfo([FromRoute] string patientId)
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
    public async Task<Credential> AddRelative([FromBody] AddNewRelativeViewModel addNewRelativeViewModel, [FromRoute] string patientId)
    {

        var result =  await _personService.AddRelative(addNewRelativeViewModel, patientId);
        return result;
    }

    [HttpPut]
    [Route("{personId}/RemoveRelationship/{patientId}")]
    public async Task<IActionResult> RemoveRelationship([FromRoute] string personId, [FromRoute] string patientId)
    {
        try
        {
            var result = await _personService.RemoveRelationship(personId, patientId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion Patient

    #region Doctor
    [HttpGet]
    [Route("DoctorInfo/{doctorId}")]
    public async Task<DoctorInfoViewModel> GetDoctorInfo([FromRoute] string doctorId)
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
    public async Task<Credential> AddNewPatient([FromBody] AddNewPatientViewModel addNewPatientViewModel, string doctorId)
    {
            var result = await _personService.AddNewPatient(addNewPatientViewModel, doctorId);
            return result;
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
    [Route("RelativeInfo/{relativeId}")]
    public async Task<RelativeInfoViewModel> GetRelativeById( [FromRoute]string relativeId)
    {
        return await _personService.GetRelativeById(relativeId);
    }
    #endregion Relatives

}
