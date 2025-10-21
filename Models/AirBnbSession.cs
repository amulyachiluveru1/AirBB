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
        public void SetFilters(AirbnbViewModel filters)
        {
            _session.SetObject(FilterKey, filters);
        }

        public AirbnbViewModel GetFilters() =>
            _session.GetObject<AirbnbViewModel>(FilterKey) ?? new AirbnbViewModel();
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
