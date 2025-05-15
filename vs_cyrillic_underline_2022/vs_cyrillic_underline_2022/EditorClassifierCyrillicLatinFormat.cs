using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace vs_cyrillic_underline_2022
{
    /// <summary>
    /// Defines an editor format for the EditorClassifierCyrillicLatin type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "EditorClassifierCyrillicLatin")]
    [Name("EditorClassifierCyrillicLatin")]
    [UserVisible(true)] // This should be visible to the end user
    [Order(Before = Priority.Default)] // Set the priority to be after the default classifiers
    internal sealed class EditorClassifierCyrillicLatinFormat : ClassificationFormatDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorClassifierCyrillicLatinFormat"/> class.
        /// </summary>
        public EditorClassifierCyrillicLatinFormat()
        {
            this.DisplayName = "EditorClassifierCyrillicLatin"; // Human readable version of the name
            this.BackgroundColor = Colors.Yellow;
            this.ForegroundColor = Colors.DarkRed;
            this.TextDecorations = System.Windows.TextDecorations.Underline;
        }
    }
}
