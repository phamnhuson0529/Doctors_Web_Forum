using Doctors_Web_Forum.BLL.IServices;
using Doctors_Web_Forum.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Doctors_Web_Forum.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TopicsController : Controller
    {
        private readonly ITopicService _topicService;

        public TopicsController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        // GET : Topics
        public async Task<IActionResult> Index(int pg = 1 , int pageSize = 5 )
        {
            var (topics, pager) = await _topicService.GetAllTopicsAsync(pg, pageSize);
            ViewBag.Pager = pager; 
            return View(topics);
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        // GET: Topics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Topics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TopicName,Description,Status")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                await _topicService.AddTopicAsync(topic);
                return RedirectToAction(nameof(Index));
            }
            return View(topic);
        }

        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        // POST: Topics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TopicName,Description,Status")] Topic topic)
        {
            if (id != topic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _topicService.UpdateTopicAsync(topic);
                    return RedirectToAction(nameof(Index));
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating topic: {ex.Message}");
                }
            }
            return View(topic);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var topic = await _topicService.GetTopicByIdAsync(id);
                if (topic == null)
                {
                    TempData["error"] = "Chủ đề không tồn tại hoặc đã bị xóa!";
                    return RedirectToAction("Index");
                }

                // Xóa chủ đề
                var isDeleted = await _topicService.DeleteTopicAsync(id);
                if (!isDeleted)
                {
                    TempData["error"] = "Có lỗi đã xảy ra khi xóa chủ đề!";
                    return RedirectToAction("Index");
                }

                // Thông báo thành công
                TempData["success"] = "Xóa chủ đề thành công!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần thiết
                TempData["error"] = $"Đã xảy ra lỗi không mong muốn: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
