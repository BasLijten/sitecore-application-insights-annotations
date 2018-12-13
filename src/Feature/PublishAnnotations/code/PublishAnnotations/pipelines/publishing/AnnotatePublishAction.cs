using Annotations;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Sitecore.Data;
using Sitecore.Data.Engines;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Publishing.Pipelines.Publish;
using System;
using System.Configuration;

namespace PublishAnnotations.Pipelines.Publishing
{
    public class AnnotatePublishAction : PublishProcessor
    {        
        public override void Process(PublishContext context)
        {
            var client = new TelemetryClient(TelemetryConfiguration.Active);

            var dependency = new DependencyTelemetry();
            dependency.Name = "Annotate publish action";
            dependency.Target = "Application Insights";
            dependency.Type = "Http";
            var operation = client.StartOperation(dependency);

            try
            {
                this.AnnotatePublishingAction();
                dependency.ResultCode = 200.ToString();
                dependency.Success = true;

            }
            catch (Exception e)
            {
                dependency.Success = false;
                client.TrackException(e);
            }
            finally
            {
                client.StopOperation(operation);
            }          
        }

        public void AnnotatePublishingAction()
        {
            var annotation = new Annotations.Annotations();
            annotation.CreateAnnotation("Published content", AICategory.Info);
        }
    }
}