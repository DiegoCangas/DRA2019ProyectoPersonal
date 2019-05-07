package ual.dra.surbus_api.modelos;

import java.util.Set;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.ManyToMany;

@Entity
public class Bus {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    String nombre;
    int numBuses;
    int numLinea;
    private String imagen;

    @ManyToMany(mappedBy = "lineas")
    Set<Parada> paradas;
    
    public Bus(){

    }

    /**
     * @return the nombre
     */
    public String getNombre() {
        return nombre;
    }

    /**
     * @param nombre the nombre to set
     */
    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    /**
     * @return the numBuses
     */
    public int getNumBuses() {
        return numBuses;
    }

    /**
     * @param numBuses the numBuses to set
     */
    public void setNumBuses(int numBuses) {
        this.numBuses = numBuses;
    }

    /**
     * @return the numLinea
     */
    public int getNumLinea() {
        return numLinea;
    }

    /**
     * @param numLinea the numLinea to set
     */
    public void setNumLinea(int numLinea) {
        this.numLinea = numLinea;
    }

    /**
     * @return the imagen
     */
    public String getImagen() {
        return imagen;
    }

    /**
     * @param imagen the imagen to set
     */
    public void setImagen(String imagen) {
        this.imagen = imagen;
    }
    
}