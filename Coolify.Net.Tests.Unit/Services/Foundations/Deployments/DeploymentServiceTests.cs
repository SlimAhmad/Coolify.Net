// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq.Expressions;
using System.Net;
using Coolify.Net.Brokers.CoolifyApis;
using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Externals.Deployments;
using Coolify.Net.Models.Foundations.Deployments;
using Coolify.Net.Models.Foundations.Deployments.Exceptions;
using Coolify.Net.Services.Foundations.Deployments;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Deployments
{
    public partial class DeploymentServiceTests
    {
        private readonly Mock<ICoolifyApiBroker> coolifyApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IDeploymentService deploymentService;

        public DeploymentServiceTests()
        {
            this.coolifyApiBrokerMock = new Mock<ICoolifyApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.deploymentService = new DeploymentService(
                coolifyApiBroker: this.coolifyApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static ExternalDeployment CreateRandomExternalDeployment() =>
            new ExternalDeployment
            {
                Uuid = GetRandomString(),
                ApplicationUuid = GetRandomString(),
                ServerUuid = GetRandomString(),
                Status = GetRandomString(),
                Logs = GetRandomString(),
                CommitSha = GetRandomString(),
                Branch = GetRandomString(),
                ForceRebuild = true,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

        private static Deployment ConvertToDeployment(ExternalDeployment externalDeployment) =>
            new Deployment
            {
                Uuid = externalDeployment.Uuid,
                ApplicationUuid = externalDeployment.ApplicationUuid,
                ServerUuid = externalDeployment.ServerUuid,
                Status = externalDeployment.Status,
                Logs = externalDeployment.Logs,
                CommitSha = externalDeployment.CommitSha,
                Branch = externalDeployment.Branch,
                ForceRebuild = externalDeployment.ForceRebuild,
                CreatedAt = externalDeployment.CreatedAt,
                UpdatedAt = externalDeployment.UpdatedAt
            };

        private static HttpRequestException CreateHttpRequestException(HttpStatusCode statusCode) =>
            new HttpRequestException(
                message: "HTTP error occurred.",
                inner: null,
                statusCode: statusCode);

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static DeploymentDependencyValidationException CreateInvalidDeploymentDependencyValidationException(
            HttpRequestException httpRequestException)
        {
            var invalidDeploymentException = new InvalidDeploymentException(
                message: "Invalid deployment.",
                innerException: httpRequestException);

            return new DeploymentDependencyValidationException(
                message: "Deployment dependency validation error occurred, fix the errors and try again.",
                innerException: invalidDeploymentException);
        }

        private static DeploymentDependencyValidationException CreateAlreadyExistsDeploymentDependencyValidationException(
            HttpRequestException httpRequestException)
        {
            var alreadyExistsDeploymentException = new AlreadyExistsDeploymentException(
                message: "Deployment already exists.",
                innerException: httpRequestException);

            return new DeploymentDependencyValidationException(
                message: "Deployment dependency validation error occurred, fix the errors and try again.",
                innerException: alreadyExistsDeploymentException);
        }

        private static DeploymentDependencyException CreateFailedDeploymentDependencyException(
            HttpRequestException httpRequestException)
        {
            var failedDeploymentDependencyException = new FailedDeploymentDependencyException(
                message: "Failed deployment dependency error occurred.",
                innerException: httpRequestException);

            return new DeploymentDependencyException(
                message: "Deployment dependency error occurred, contact support.",
                innerException: failedDeploymentDependencyException);
        }

        private static DeploymentServiceException CreateFailedDeploymentServiceException(Exception exception)
        {
            var failedDeploymentServiceException = new FailedDeploymentServiceException(
                message: "Failed deployment service error occurred.",
                innerException: exception);

            return new DeploymentServiceException(
                message: "Deployment service error occurred, contact support.",
                innerException: failedDeploymentServiceException);
        }

        private static DeploymentDependencyException CreateFailedDeploymentDependencyExceptionFromTimeout(
            OperationCanceledException operationCanceledException)
        {
            var timeoutDeploymentException = new TimeoutDeploymentException(
                message: "Deployment dependency timeout error occurred.",
                innerException: operationCanceledException);

            return new DeploymentDependencyException(
                message: "Deployment dependency error occurred, contact support.",
                innerException: timeoutDeploymentException);
        }
    }
}
