package ual.dra.surbus_api.repositories;

import java.util.Set;

import org.jboss.logging.Param;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.data.rest.core.annotation.RepositoryRestResource;

import ual.dra.surbus_api.modelos.Parada;


@RepositoryRestResource()
public interface ParadaRepository extends CrudRepository<Parada, Long>{

   @Query(value = "Select * From parada Where name LIKE '%?1%'", nativeQuery = true)
   Set<Parada> findByName(String nombre);
  // @Query(value = "Select * From parada Where numeroParada = ?1", nativeQuery = true)
   //Set<Parada> findByNombre(Integer numParada);
}