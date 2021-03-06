﻿using Axle.References;
using Axle.Resources.Extraction;
using Axle.Verification;

namespace Axle.Resources.ResX.Extraction
{
    /// <summary>
    /// An implementation of the <see cref="IResourceExtractor"/> interface that is capable
    /// of handling the .NET's resx resource format.
    /// </summary>
    /// <inheritdoc />
    public sealed class ResXResourceExtractor : AbstractResourceExtractor
    {
        protected override Nullsafe<ResourceInfo> DoExtract(ResourceContext context, string name)
        {
            context.VerifyArgument(nameof(context)).IsNotNull();
            name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();

            IResourceExtractor actualExtractor = null;

            // The extent of ResX support varies depending on the target framework
            #if NETFRAMEWORK
            actualExtractor = new CompleteResXResourceExtractor();
            #elif NETSTANDARD2_0_OR_NEWER
            actualExtractor = new SimpleResXResourceExtractor();
            #elif NETSTANDARD1_3_OR_NEWER
            actualExtractor = new TextResXResourceExtractor();
            #endif
            return actualExtractor != null 
                ? actualExtractor.Extract(context, name)
                : Nullsafe<ResourceInfo>.None;
        }
    }
}
