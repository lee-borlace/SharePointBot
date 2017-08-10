using Autofac;
using Microsoft.Bot.Builder.Internals.Fibers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharePointBot.Dialogs
{
    /// <summary>
    /// Abstract base dialog class to provide several pieces of Autofac functionality.
    /// </summary>
    [Serializable]
    public abstract class AutofacDialog
    {
        [NonSerialized]
        protected readonly ILifetimeScope _dialogScope;

        public AutofacDialog(ILifetimeScope dialogScope)
        {
            SetField.NotNull(out this._dialogScope, nameof(dialogScope), dialogScope);
        }
    }
}