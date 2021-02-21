using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsUnlimited.InventoryAPI.Configuration
{
    public class TracingConfiguration
    {
        public int Exporter { get; set; }
        public string ZipkinEndpoint { get; set; }
        public JaegerConfiguraton JaegerEndpoint { get; set; }
        public string OltpEndpoint { get; set; }
    }

    public class JaegerConfiguraton
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public enum TraceExporterOptions
    {
        JAEGER = 0,
        ZIPKIN = 1,
        OPENTELEMETRY_PROTOCOL = 2
    }
}