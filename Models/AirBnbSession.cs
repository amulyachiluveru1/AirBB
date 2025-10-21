namespace AirBB.Models
{
    public class AirBnbSession
    {
        private const string FilterKey = "filters";
        private const string ReservationKey = "reservations";
        private const string CountKey = "reservationCount";

        private readonly ISession _session;

        public AirBnbSession(ISession session)
        {
            _session = session;
        }
        public void SetFilters(AirBnbViewModel filters)
        {
            _session.SetObject(FilterKey, filters);
        }

        public AirBnbViewModel GetFilters() =>
            _session.GetObject<AirBnbViewModel>(FilterKey) ?? new AirBnbViewModel();
        public void SetReservations(List<Reservation> reservations)
        {
            _session.SetObject(ReservationKey, reservations);
            _session.SetInt32(CountKey, reservations.Count);
        }

        public List<Reservation> GetReservations() =>
            _session.GetObject<List<Reservation>>(ReservationKey) ?? new List<Reservation>();

        public int? GetReservationCount() => _session.GetInt32(CountKey);

        public void RemoveReservations()
        {
            _session.Remove(ReservationKey);
            _session.Remove(CountKey);
        }
    }
}
