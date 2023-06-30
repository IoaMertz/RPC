﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Application.Commands
{
    public class CalculationRequestCommand : IRequest<bool>
    {
        public int Number { get; set; }
        public string ServiceName { get; set; }

        public CalculationRequestCommand(int number, string serviceName )
        {
            Number = number;
            ServiceName = serviceName;
        }

    }
}
