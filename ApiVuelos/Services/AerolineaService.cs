using ApiVuelos.DTOs;
using ApiVuelos.Models;
using ApiVuelos.Repository;
using AutoMapper;

namespace ApiVuelos.Services
{
    public class AerolineaService : ICommonService<AerolineaDto, AerolineaDto, AerolineaDto>
    {
        private readonly IRepository<Aerolinea> _repository;

        private readonly IMapper _mapper;

        public AerolineaService(IRepository<Aerolinea> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<AerolineaDto>> Get()
        {
            var aerolinea = await _repository.Get();
            return aerolinea.Select(a => _mapper.Map<AerolineaDto>(a));
        }

        public async Task<AerolineaDto> GetById(int id)
        {
            var aerolinea = await _repository.GetById(id);
            if (aerolinea != null)
            {
                var aerolineaDto = _mapper.Map<AerolineaDto>(aerolinea);
                return aerolineaDto;
            }
            return null;
        }

        public async Task<AerolineaDto> Add(AerolineaDto crearAerolineaDto)
        {
            var aerolinea = _mapper.Map<Aerolinea>(crearAerolineaDto);
            await _repository.Add(aerolinea);
            await _repository.Save();
            var aerolineaDto = _mapper.Map<AerolineaDto>(aerolinea);
            return aerolineaDto;
        }

        public async Task<AerolineaDto> Update(int id, AerolineaDto modAerolineaDto)
        {
            var aerolinea = await _repository.GetById(id);
            if (aerolinea != null)
            {
                aerolinea.Nombre = modAerolineaDto.Nombre;

                _repository.Update(aerolinea);
                await _repository.Save();

                var aerolineaDto = _mapper.Map<AerolineaDto>(aerolinea);

                return aerolineaDto;
            }

            return null;
        }

        public async Task<AerolineaDto> Delete(int id)
        {
            var aerolinea = await _repository.GetById(id);

            if(aerolinea != null)
            {
                var aerolineaDto = _mapper.Map<AerolineaDto>(aerolinea);

                _repository.Delete(aerolinea);
                await _repository.Save();
                return aerolineaDto;
            }
            return null;
        }

    }
}
