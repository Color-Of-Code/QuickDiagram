﻿using System.Threading.Tasks;

namespace Codartis.SoftVis.VisualStudioIntegration.App.Commands
{
    /// <summary>
    /// Base class for commands that can be invoked asynchronously without a parameter.
    /// </summary>
    internal abstract class AsyncCommandWithoutParameterBase : AsyncCommandBase
    {
        protected AsyncCommandWithoutParameterBase(IAppServices appServices)
            : base(appServices)
        {
        }

        public abstract Task ExecuteAsync();
    }
}