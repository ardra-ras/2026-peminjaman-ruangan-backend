using BookingRoom.Data;
using BookingRoom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _db;

    public BookingsController(AppDbContext db)
    {
        _db = db;
    }

    // CREATE
    [HttpPost]
    public async Task<ActionResult<Booking>> CreateBooking([FromBody] Booking booking)
    {
        if (booking.EndTime <= booking.StartTime)
            return BadRequest("EndTime must be after StartTime.");

        // 🔥 VALIDASI BENTROK RUANGAN
        var conflict = await _db.Bookings.AnyAsync(b =>
            b.RoomName == booking.RoomName &&
            booking.StartTime < b.EndTime &&
            booking.EndTime > b.StartTime
        );

        if (conflict)
        {
            return BadRequest("Room already booked at that time");
        }

        booking.Status = string.IsNullOrWhiteSpace(booking.Status) ? "pending" : booking.Status;

        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
    }


    // READ (Detail) - dipakai buat CreatedAtAction
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Booking>> GetBookingById(int id)
    {
        var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        if (booking == null) return NotFound();
        return Ok(booking);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Booking>>> GetAll()
    {
        var bookings = await _db.Bookings
            .OrderByDescending(b => b.Id)
            .ToListAsync();

        return Ok(bookings);
    }

    // UPDATE
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Booking>> UpdateBooking(int id, [FromBody] Booking updated)
    {
        var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        if (booking == null) return NotFound();

        if (updated.EndTime <= updated.StartTime)
            return BadRequest("EndTime must be after StartTime.");

        booking.BorrowerName = updated.BorrowerName;
        booking.RoomName = updated.RoomName;
        booking.StartTime = updated.StartTime;
        booking.EndTime = updated.EndTime;
        booking.Status = string.IsNullOrWhiteSpace(updated.Status) ? booking.Status : updated.Status;
        booking.Purpose = updated.Purpose;

        await _db.SaveChangesAsync();

        return Ok(booking);
    }

    // DELETE
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteBooking(int id)
    {
        var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        if (booking == null) return NotFound();

        _db.Bookings.Remove(booking);
        await _db.SaveChangesAsync();

        return Ok("Deleted");
    }

}
