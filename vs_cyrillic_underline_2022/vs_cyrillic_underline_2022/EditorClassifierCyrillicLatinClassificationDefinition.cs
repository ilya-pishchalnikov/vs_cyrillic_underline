using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace vs_cyrillic_underline_2022
{
    /// <summary>
    /// Classification type definition export for EditorClassifierCyrillicLatin
    /// </summary>
    internal static class EditorClassifierCyrillicLatinClassificationDefinition
    {
        // This disables "The field is never used" compiler's warning. Justification: the field is used by MEF.
#pragma warning disable 169

        /// <summary>
        /// Defines the "EditorClassifierCyrillicLatin" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("EditorClassifierCyrillicLatin")]
        private static ClassificationTypeDefinition typeDefinition;

#pragma warning restore 169
    }
}
