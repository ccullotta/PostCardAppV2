using Microsoft.Extensions.DependencyInjection;
using PostCardAppV2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PostCardAppV2.Backend.CronJobs
{
    public class QuotesRecycleJob : CronJobService
    {
        private IEmailService _service;

        public QuotesRecycleJob(IScheduleConfig<QuotesRecycleJob> config, IEmailService service) : base(config.CronExpression, config.TimeZoneInfo)
        {
            _service = service;
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {

            _service.sendCsvRecycleEmail();
            return base.DoWork(cancellationToken);
        }

    }

    public interface IScheduleConfig<T>
    {
        string CronExpression { get; set; }
        TimeZoneInfo TimeZoneInfo { get; set; }
    }

    public class ScheduleConfig<T> : IScheduleConfig<T>
    {
        public string CronExpression { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
    }
}
