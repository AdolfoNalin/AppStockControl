namespace AppStockControl.Helpers
{
    public class MaskBehavior : Behavior<Entry>
    {
        List<char> _lastFormatted = new List<char>();

        public static readonly BindableProperty MaskProperty =
         BindableProperty.Create(nameof(Mask), typeof(string), typeof(MaskBehavior), string.Empty);

        public string Mask
        {
            get => (string)GetValue(MaskProperty);
            set => SetValue(MaskProperty, value);
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnTextChanged;
            base.OnDetachingFrom(entry);
        }
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not Entry entry || string.IsNullOrEmpty(Mask)) return;

            int cursorPos = entry.CursorPosition;
            string oldText = _lastFormatted.ToString() ?? String.Empty; // guardado do ciclo anterior
            string newText = e.NewTextValue ?? string.Empty;

            // 1. Descobre quantos dígitos existem ANTES do cursor no texto novo
            int digitsBeforeCursor = newText.Substring(0, Math.Min(cursorPos, newText.Length))
                                             .Count(char.IsDigit);

            // 2. Extrai só os dígitos do texto inteiro
            string digits = new string(newText.Where(char.IsDigit).ToArray());

            // 3. Remonta com a máscara
            string formatted = ApplyMask(digits, Mask);

            // 4. Calcula onde o cursor deve ficar: anda na máscara até "gastar" 
            //    a mesma quantidade de dígitos que tinha antes do cursor
            int newCursorPos = CalculateCursorPosition(formatted, digitsBeforeCursor);

            if (entry.Text == formatted)
                return;

            entry.TextChanged -= OnTextChanged;
            entry.Text = formatted;
            entry.CursorPosition = newCursorPos;
            entry.TextChanged += OnTextChanged;

            _lastFormatted = formatted.ToList();
        }

        private int CalculateCursorPosition(string formatted, int digitsToSkip)
        {
            int count = 0;
            for (int i = 0; i < formatted.Length; i++)
            {
                if (count == digitsToSkip)
                    return i;

                if (char.IsDigit(formatted[i]))
                    count++;
            }
            return formatted.Length;
        }

        private string ApplyMask(string digits, string mask)
        {
            var result = new System.Text.StringBuilder();
            int digitIndex = 0;

            foreach (char maskChar in mask)
            {
                if (digitIndex >= digits.Length)
                    break;

                if (maskChar == '#')
                {
                    result.Append(digits[digitIndex]);
                    digitIndex++;
                }
                else
                {
                    result.Append(maskChar);
                }
            }

            return result.ToString();
        }
    }
}
