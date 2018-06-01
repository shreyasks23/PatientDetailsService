using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using PatientDataAccess;


namespace PatientDetailsService.Controllers
{
    [BasicAuthentication]
    public class PatientDetailsController : ApiController
    {
        //Get all patient details 
        //@Path: api/patientdetails/
        [Authorize(Roles = "admin")]
        public HttpResponseMessage GetPatientDetails()
        {
            try
            {
                using (PatientsDBEntities entity = new PatientsDBEntities())
                {
                    var entities = entity.Patients.ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, entities);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        //Get particular patient details using Patient_ID 
        //@Path: api/patientdetails/{id}
        [Authorize(Roles = "admin,user")]
        public HttpResponseMessage GetPatientDetail(int id)
        {
            try
            {
                using (PatientsDBEntities entity = new PatientsDBEntities())
                {
                    var entities = entity.Patients.FirstOrDefault(e => e.Patient_ID == id);

                    if (entities != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entities);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient with ID: " + id + " is not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Insert new patient details
        //@Path: api/patientdetails/
        [Authorize(Roles = "admin,user")]
        public HttpResponseMessage Post([FromBody]Patient patientDetails)
        {
            try
            {
                using (PatientsDBEntities entity = new PatientsDBEntities())
                {
                    entity.Patients.Add(patientDetails);
                    entity.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, patientDetails);
                    message.Headers.Location = new Uri(Request.RequestUri + patientDetails.Patient_ID.ToString());

                    return message;
                }
            }
            catch (DbEntityValidationException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        //update existing patient details
        //@Path: api/patientdetails/{id}
        [Authorize(Roles = "admin,user")]
        public HttpResponseMessage Put(int id, [FromBody]Patient patientDetails)
        {
            try
            {
                using (PatientsDBEntities entity = new PatientsDBEntities())
                {
                    if (patientDetails != null)
                    {
                        var entities = entity.Patients.FirstOrDefault(p => p.Patient_ID == id);

                        entities.Age = patientDetails.Age;
                        entities.BloodPressure = patientDetails.BloodPressure;
                        entities.BMI = patientDetails.BMI;
                        entities.BodyFat = patientDetails.BodyFat;
                        entities.BodyWater = patientDetails.BodyWater;
                        entities.BoneMass = patientDetails.BoneMass;
                        entities.ECG = patientDetails.ECG;
                        entities.EyeTest = patientDetails.EyeTest;
                        entities.Gender = patientDetails.Gender;
                        entities.HaemoglobinCount = patientDetails.HaemoglobinCount;
                        entities.MuscleMass = patientDetails.MuscleMass;
                        entities.Hip = patientDetails.Hip;
                        entities.Height = patientDetails.Height;
                        entities.ECG = patientDetails.ECG;
                        entities.IdealBodyWeight = patientDetails.IdealBodyWeight;
                        entities.Patient_Location = patientDetails.Patient_Location;
                        entities.Patient_Name = patientDetails.Patient_Name;
                        entities.ExaminedOn = patientDetails.ExaminedOn;
                        entities.SkinTest = patientDetails.SkinTest;
                        entities.SPO2 = patientDetails.SPO2;
                        entities.VaccinationHB = patientDetails.VaccinationHB;
                        entities.VaccinationTyphoid = patientDetails.VaccinationTyphoid;
                        entities.Waist = patientDetails.Waist;
                        entities.WaistHeightRatio = patientDetails.WaistHeightRatio;
                        entities.WaistHipRatio = patientDetails.WaistHipRatio;

                        entity.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entities);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient with ID: " + patientDetails.Patient_ID + "not found to update");
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Delete patients of particular ID
        [Authorize(Roles = "admin")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (PatientsDBEntities entities = new PatientsDBEntities())
                {
                    var entity = entities.Patients.FirstOrDefault(p => p.Patient_ID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID = " + id + "not found");
                    }

                    else
                    {
                        entities.Patients.Remove(entity);
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }

            }
            catch (DbEntityValidationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
