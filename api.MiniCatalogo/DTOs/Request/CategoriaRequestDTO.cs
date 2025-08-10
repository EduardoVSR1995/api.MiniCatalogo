using api.MiniCatalogo.Config.Const;
using System.ComponentModel.DataAnnotations;

namespace api.MiniCatalogo.DTOs.Request
{
    public class CategoriaRequestDTO
    {
        private string _nome = null!;
        /// <summary>
        /// Category name.
        /// </summary>
        [Required(ErrorMessage = Messages._requiredName)]
        public string Nome
        {
            get => _nome;
            set
            {
                var normalized = value?.Trim();

                if (!string.IsNullOrEmpty(normalized))
                {
                    normalized = char.ToUpper(normalized[0]) + normalized.Substring(1).ToLower();
                }

                _nome = normalized;
            }
        }
    }
}
