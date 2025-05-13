using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace vs_cyrillic_underline_2022
{
    /// <summary>
    /// Classification type definition export for EditorClassifierCyrillic
    /// </summary>
    internal static class EditorClassifierCyrillicClassificationDefinition
    {
        // This disables "The field is never used" compiler's warning. Justification: the field is used by MEF.
#pragma warning disable 169

        /// <summary>
        /// Defines the "EditorClassifierCyrillic" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("EditorClassifierCyrillic1")]
        private static ClassificationTypeDefinition typeDefinition;

#pragma warning restore 169
    }
}
