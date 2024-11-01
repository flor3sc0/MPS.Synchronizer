namespace MPS.Synchronizer.Application.CommonModels
{
    public class JwtToken
    {
        /// <summary>
        /// Уникальный идентификатор токена (UUIDv4).
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Битовая маска свойств токена.
        /// </summary>
        public uint S { get; set; }

        /// <summary>
        /// Уникальный идентификатор продавца на WB, которому принадлежит токен (UUIDv4).
        /// </summary>
        public Guid Sid { get; set; }

        /// <summary>
        /// Время жизни токена, соответствующее стандарту RFC 7519 (JSON Web Token).
        /// </summary>
        public DateTime Exp { get; set; }

        /// <summary>
        /// Тестовый контур (песочница).
        /// </summary>
        public bool T { get; set; }
    }
}