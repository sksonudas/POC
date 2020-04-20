namespace ProjectDetailService.Services
{
    using Microsoft.AspNetCore.Http;
    using ProjectDetailService.Models;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;

    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IdentityModel GetIdentity()
        {
            string authorizationHeader = _context.HttpContext.Request.Headers["Authorization"];

            if (authorizationHeader != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = authorizationHeader.Split(" ")[1];
                var paresedToken = tokenHandler.ReadJwtToken(token);

                var empid = paresedToken.Claims
                    .Where(c => c.Type == "empid")
                    .FirstOrDefault();

                var designation = paresedToken.Claims
                    .Where(c => c.Type == "designation")
                    .FirstOrDefault();

                var name = paresedToken.Claims
                    .Where(c => c.Type == "name")
                    .FirstOrDefault();

                return new IdentityModel()
                {
                    EmpId = Convert.ToInt32(empid.Value),
                    Designation = designation.Value,
                    FullName = name.Value

                };
            }

            throw new ArgumentNullException("employeenumber");
        }

       
    }
}
