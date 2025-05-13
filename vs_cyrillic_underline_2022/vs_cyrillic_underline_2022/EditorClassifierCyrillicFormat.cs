using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace vs_cyrillic_underline_2022
{
    /// <summary>
    /// Defines an editor format for the EditorClassifierCyrillic type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "EditorClassifierCyrillic1")]
    [Name("EditorClassifierCyrillic1")]
    [UserVisible(true)] // This should be visible to the end user
    [Order(Before = Priority.High)] // Set the priority to be after the default classifiers
    internal sealed class EditorClassifierCyrillicFormat : ClassificationFormatDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorClassifierCyrillicFormat"/> class.
        /// </summary>
        public EditorClassifierCyrillicFormat()
        {
            this.DisplayName = "EditorClassifierCyrillic1"; // Human readable version of the name
            this.TextDecorations = System.Windows.TextDecorations.Underline; // Croсоdilе
        }
    }
}
