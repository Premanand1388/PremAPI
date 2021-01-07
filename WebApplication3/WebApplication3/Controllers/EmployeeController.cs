using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers

{   
    public class EmployeeController : ApiController
    {
        private dataInstance db = new dataInstance();
        public HttpResponseMessage InsertData([System.Web.Http.FromBody] GetEmployee getEmployee)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString);
            try
            {
                SqlCommand sqlCommand = new SqlCommand("Insert into UserData(FirstName, LastName, Email, DOB) values('" + getEmployee.FirstName + " ," + getEmployee.LastName + " ," + getEmployee.Email + " ," + getEmployee.Dob);
                int i = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                var message = Request.CreateResponse(HttpStatusCode.Created, getEmployee);
                message.Headers.Location = new Uri(Request.RequestUri + getEmployee.Email);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }

        public IQueryable<GetEmployee> GetEmployees()
        {
            return db.Employee;
        }

        // Get's Prem Detail
        
        [ResponseType(typeof(GetEmployee))]

        public async Task<IHttpActionResult> GetDetails(string id)
        {
            GetEmployee getEmployee = await db.Employee.FindAsync(id);
            if (getEmployee == null)
            {
                return NotFound();
            }
            return Ok(getEmployee);
        }
    }


}
