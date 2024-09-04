namespace Test_ex.Maps
{
    public interface IMapper<Model,DTO>
    {
        public Model Map(DTO dto);
        public DTO Map(Model model);
        public Model UpdateMap(Model model, DTO dto);
        public IEnumerable<DTO> MapList(IEnumerable<Model> models);
    }
}
