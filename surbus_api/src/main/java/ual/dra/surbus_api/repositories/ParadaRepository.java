package ual.dra.surbus_api.repositories;

import java.util.Set;

import org.springframework.data.repository.CrudRepository;
import org.springframework.data.rest.core.annotation.RepositoryRestResource;

import ual.dra.surbus_api.modelos.Parada;
@RepositoryRestResource()
public interface ParadaRepository extends CrudRepository<Parada, Long>{

//Set<Parada> findByNombreContaning(String nombre);
}