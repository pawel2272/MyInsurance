﻿using MyInsurance.BusinessLogic.Data;
using System;

namespace MyInsurance.BusinessLogic.Services.ServiceInterfaces
{
    public interface IPolicyService : IDisposable
    {
        void Add(int customerId, int employeeId, decimal amount, string type, string name, DateTime dateOfEnding);
        Policy GetPolicy(int policyId);
        Customer GetPolicyCustomer(int policyId);
        Employee GetPolicyEmployee(int policyId);
    }
}
