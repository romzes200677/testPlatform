namespace CSharpTestApp.Controllers
{
    public class CodeExecutionRequest
    {
        /// <summary>
        /// Идентификатор задания (для получения соответствующих тестов)
        /// </summary>
        public string AssignmentId { get; set; }

        /// <summary>
        /// Текст задания с поддержкой Markdown
        /// </summary>
        public string AssignmentText { get; set; }

        /// <summary>
        /// Шаблон метода Main с плейсхолдером ###
        /// </summary>
        public string MainMethodTemplate { get; set; }

        /// <summary>
        /// Код пользователя для вставки в плейсхолдер ###
        /// </summary>
        public string UserCode { get; set; }
    }
}