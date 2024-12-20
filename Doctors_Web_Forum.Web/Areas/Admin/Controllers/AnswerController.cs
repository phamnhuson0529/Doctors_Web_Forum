using Doctors_Web_Forum.BLL.IServices;
using Doctors_Web_Forum.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Doctors_Web_Forum.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AnswerController : Controller
    {
        private readonly IAnswerService _answerService;
        private readonly IQuestionService _questionService;
        private readonly UserManager<User> _userManager; // Inject UserManager

        public AnswerController(IAnswerService answerService, IQuestionService questionService, UserManager<User> userManager)
        {
            _answerService = answerService;
            _questionService = questionService;
            _userManager = userManager; // Khởi tạo UserManager
        }

        // GET: Admin/Answer/Create/5 (Để trả lời câu hỏi)
        public async Task<IActionResult> Create(int questionId)
        {
            var question = await _questionService.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                // Nếu không tìm thấy câu hỏi, trả về trang chủ hoặc trang lỗi
                TempData["ErrorMessage"] = "Câu hỏi không tồn tại!";
                return RedirectToAction("Index", "Question", new { area = "Admin" });
            }

            // Trả về trang tạo câu trả lời với thông tin câu hỏi
            ViewBag.QuestionId = question.Id;
            ViewBag.QuestionText = question.QuestionText;
            return View();
        }

        // POST: Admin/Answer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int questionId, [Bind("AnswerText")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                // Lấy thông tin câu hỏi
                var currentQuestion = await _questionService.GetQuestionByIdAsync(questionId);
                if (currentQuestion == null)
                {
                    // Nếu không tìm thấy câu hỏi, trả về trang chủ hoặc trang lỗi
                    TempData["ErrorMessage"] = "Câu hỏi không tồn tại!";
                    return RedirectToAction("Index", "Question", new { area = "Admin" });
                }

                // Lấy UserId từ người dùng hiện tại
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    answer.UserId = currentUser.Id; // Gán UserId từ người dùng đã đăng nhập
                }
                else
                {
                    TempData["ErrorMessage"] = "Không tìm thấy người dùng hiện tại!";
                    return RedirectToAction("Index", "Question", new { area = "Admin" });
                }

                // Gán QuestionId
                answer.QuestionId = questionId;

                // Tạo câu trả lời mới
                var newAnswer = await _answerService.CreateAnswerAsync(answer);

                // Thông báo thành công
                TempData["SuccessMessage"] = "Câu trả lời đã được tạo thành công!";
                return RedirectToAction("Index", "Question", new { area = "Admin" });
            }

            // Nếu có lỗi, giữ lại thông tin câu hỏi và trả lại trang tạo câu trả lời
            var question = await _questionService.GetQuestionByIdAsync(questionId);
            ViewBag.QuestionId = questionId;
            ViewBag.QuestionText = question.QuestionText;
            return View(answer);
        }

        // GET: Admin/Answer/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var answer = await _answerService.GetAnswerByIdAsync(id);
            if (answer == null)
            {
                // Nếu không tìm thấy câu trả lời, trả về trang chủ hoặc trang lỗi
                TempData["ErrorMessage"] = "Câu trả lời không tồn tại!";
                return RedirectToAction("Index", "Question", new { area = "Admin" });
            }

            return View(answer);
        }

        // POST: Admin/Answer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, AnswerText")] Answer answer)
        {
            if (id != answer.Id)
            {
                // Nếu Id không trùng khớp, trả về trang lỗi
                TempData["ErrorMessage"] = "Không tìm thấy câu trả lời!";
                return RedirectToAction("Index", "Question", new { area = "Admin" });
            }

            if (ModelState.IsValid)
            {
                var updatedAnswer = await _answerService.UpdateAnswerAsync(id, answer.AnswerText);
                if (updatedAnswer == null)
                {
                    TempData["ErrorMessage"] = "Câu trả lời không tồn tại!";
                    return RedirectToAction("Index", "Question", new { area = "Admin" });
                }

                // Thông báo thành công
                TempData["SuccessMessage"] = "Câu trả lời đã được cập nhật thành công!";
                return RedirectToAction("Index", "Question", new { area = "Admin" });
            }

            return View(answer);
        }

        // GET: Admin/Answer/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _answerService.DeleteAnswerAsync(id);
            if (!result)
            {
                TempData["ErrorMessage"] = "Không thể xóa câu trả lời! Câu trả lời không tồn tại hoặc đã xảy ra lỗi.";
                return RedirectToAction("Index", "Question", new { area = "Admin" });
            }

            TempData["SuccessMessage"] = "Câu trả lời đã được xóa thành công!";
            return RedirectToAction("Index", "Question", new { area = "Admin" });
        }

    }
}
