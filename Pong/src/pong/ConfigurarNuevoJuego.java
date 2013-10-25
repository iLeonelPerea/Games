/**********************************************************************/
/* Autor: Francisco I. Leyva
 * P�gina web: http://www.panchosoft.com
 * Correo electr�nico: yagami_2@hotmail.com
 *
 *
/**********************************************************************/
package pong;
import javax.swing.*;

public class ConfigurarNuevoJuego extends javax.swing.JFrame {
    
    /*Juego del gato donde se aplicar� este modelo.*/
    private Mundo3D padre;
    private JOptionPane mensaje;
    /*Datos del modelo.*/
    public final int HOMBREvsHOMBRE = 13;
    public final int HOMBREvsCOMPUTADORA = 14;
    public int tipo_juego = 0, goles;
    public String nombre1, nombre2;
    
    /** Crea un nuevo Modelo */
    public ConfigurarNuevoJuego( Mundo3D mundo ) {
        /*Iniciamos componentes visuales.*/
        initComponents();
        setVisible(true);
        mensaje = new JOptionPane();
        
        /*Asignamos el gato.*/
        this.padre = mundo;
        hvpc.setEnabled(false);
    }
    
    /*M�todo que recoje los datos.*/
    public boolean recojer(){

        /*Comprobamos que los campos est�n llenos.*/
        if( this.txtJugador1.getText().equals("") ){
            mensaje.showMessageDialog(this,"Llene el nombre del jugador 1 por favor.","[X] Error:",JOptionPane.ERROR_MESSAGE);
            return false;   
        }
        if( this.txtJugador2.getText().equals("") && this.hvsh.isSelected() ){
            mensaje.showMessageDialog(this,"Llene el nombre del jugador 2 por favor.","[X] Error:",JOptionPane.ERROR_MESSAGE);
            return false;   
        }
        if( this.txtJugador1.getText().equals( this.txtJugador2.getText() )){
            mensaje.showMessageDialog(this,"Escriba nombres diferentes para los jugadores.","[X] Error:",JOptionPane.ERROR_MESSAGE);
            return false;
        }

        try{
            goles = Integer.parseInt(this.txtGoles.getText());
        }
        catch(Exception ex){
            mensaje.showMessageDialog(this,"Escriba un número de goles válido.","[X] Error:",JOptionPane.ERROR_MESSAGE);
        }
        
        /*Recojemos los valores.*/
        this.tipo_juego = ( this.hvsh.isSelected() ) ? HOMBREvsHOMBRE : HOMBREvsCOMPUTADORA;
        this.nombre1 = this.txtJugador1.getText();
        this.nombre2 = this.txtJugador2.getText();
        
        return true;
    }
    
    /*M�todo que env�a los datos ( modelo ) al gato.*/
    public void enviarModelo(){
        padre.recojerModelo();
    }
    
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        Grupo = new javax.swing.ButtonGroup();
        opcionUno = new javax.swing.JLabel();
        hvsh = new javax.swing.JRadioButton();
        hvpc = new javax.swing.JRadioButton();
        img1 = new javax.swing.JLabel();
        img2 = new javax.swing.JLabel();
        img3 = new javax.swing.JLabel();
        opcionDos = new javax.swing.JLabel();
        jugador1 = new javax.swing.JLabel();
        txtJugador1 = new javax.swing.JTextField();
        jugador2 = new javax.swing.JLabel();
        txtJugador2 = new javax.swing.JTextField();
        jSeparator1 = new javax.swing.JSeparator();
        jSeparator2 = new javax.swing.JSeparator();
        btnAceptar = new javax.swing.JButton();
        btnCancelar = new javax.swing.JButton();
        jSeparator3 = new javax.swing.JSeparator();
        opcionDos1 = new javax.swing.JLabel();
        txtGoles = new javax.swing.JTextField();
        jMenuBar1 = new javax.swing.JMenuBar();
        jMenu1 = new javax.swing.JMenu();
        jMenuItem1 = new javax.swing.JMenuItem();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        setTitle("Modo de juego");
        setFont(new java.awt.Font("Tahoma", 1, 12)); // NOI18N
        setIconImage(new ImageIcon(this.getClass().getResource("/pong/images/gdm.png")).getImage());
        setLocationByPlatform(true);

        opcionUno.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/rating_star.png"))); // NOI18N
        opcionUno.setText("Estilo de juego:");

        Grupo.add(hvsh);
        hvsh.setSelected(true);
        hvsh.setText("Persona a Persona");
        hvsh.setBorder(javax.swing.BorderFactory.createEmptyBorder(0, 0, 0, 0));
        hvsh.setMargin(new java.awt.Insets(0, 0, 0, 0));
        hvsh.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                hvshActionPerformed(evt);
            }
        });

        Grupo.add(hvpc);
        hvpc.setText("Persona contra Computadora");
        hvpc.setBorder(javax.swing.BorderFactory.createEmptyBorder(0, 0, 0, 0));
        hvpc.setMargin(new java.awt.Insets(0, 0, 0, 0));
        hvpc.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                hvpcActionPerformed(evt);
            }
        });

        img1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/pvsp.png"))); // NOI18N

        img2.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/pvspc.png"))); // NOI18N

        img3.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/persona.png"))); // NOI18N

        opcionDos.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/rating_star.png"))); // NOI18N
        opcionDos.setText("Nombre(s) de jugador(es):");

        jugador1.setText("Jugador 1:");

        jugador2.setText("Jugador 2:");

        btnAceptar.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/checkin.png"))); // NOI18N
        btnAceptar.setMnemonic(java.awt.event.KeyEvent.VK_ENTER);
        btnAceptar.setText("Aceptar");
        btnAceptar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnAceptarActionPerformed(evt);
            }
        });

        btnCancelar.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/exit.gif"))); // NOI18N
        btnCancelar.setText("Cancelar");
        btnCancelar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnCancelarActionPerformed(evt);
            }
        });

        opcionDos1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/rating_star.png"))); // NOI18N
        opcionDos1.setText("Número de goles límite:");

        jMenu1.setText("Archivo");

        jMenuItem1.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_F4, java.awt.event.InputEvent.ALT_MASK));
        jMenuItem1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/pong/images/exit.gif"))); // NOI18N
        jMenuItem1.setText("Cerrar menú de Juego Nuevo");
        jMenuItem1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cerrarVentana(evt);
            }
        });
        jMenu1.add(jMenuItem1);

        jMenuBar1.add(jMenu1);

        setJMenuBar(jMenuBar1);

        org.jdesktop.layout.GroupLayout layout = new org.jdesktop.layout.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
            .add(layout.createSequentialGroup()
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
                    .add(layout.createSequentialGroup()
                        .add(47, 47, 47)
                        .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
                            .add(hvsh)
                            .add(layout.createSequentialGroup()
                                .add(31, 31, 31)
                                .add(img1)))
                        .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
                            .add(layout.createSequentialGroup()
                                .add(52, 52, 52)
                                .add(hvpc))
                            .add(layout.createSequentialGroup()
                                .add(84, 84, 84)
                                .add(img3)
                                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                                .add(img2))))
                    .add(layout.createSequentialGroup()
                        .add(19, 19, 19)
                        .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING, false)
                            .add(opcionUno)
                            .add(layout.createSequentialGroup()
                                .add(10, 10, 10)
                                .add(jugador1)
                                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                                .add(txtJugador1, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 103, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
                                .add(35, 35, 35)
                                .add(jugador2)
                                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                                .add(txtJugador2, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 114, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE))
                            .add(opcionDos)
                            .add(jSeparator2, org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, 417, Short.MAX_VALUE)
                            .add(jSeparator1)
                            .add(layout.createSequentialGroup()
                                .add(opcionDos1)
                                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                                .add(txtGoles, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 103, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE))))
                    .add(org.jdesktop.layout.GroupLayout.TRAILING, layout.createSequentialGroup()
                        .add(19, 19, 19)
                        .add(jSeparator3, org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, 417, Short.MAX_VALUE))
                    .add(org.jdesktop.layout.GroupLayout.TRAILING, layout.createSequentialGroup()
                        .addContainerGap(246, Short.MAX_VALUE)
                        .add(btnAceptar)
                        .add(4, 4, 4)
                        .add(btnCancelar)))
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(org.jdesktop.layout.GroupLayout.LEADING)
            .add(layout.createSequentialGroup()
                .addContainerGap(org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .add(opcionUno)
                .add(15, 15, 15)
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.BASELINE)
                    .add(img1)
                    .add(img3, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 48, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
                    .add(img2))
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.BASELINE)
                    .add(hvsh)
                    .add(hvpc))
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(jSeparator1, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 10, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
                .add(19, 19, 19)
                .add(opcionDos)
                .add(19, 19, 19)
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.BASELINE)
                    .add(jugador1)
                    .add(jugador2)
                    .add(txtJugador2, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
                    .add(txtJugador1, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE))
                .add(27, 27, 27)
                .add(jSeparator2, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 10, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.BASELINE)
                    .add(opcionDos1)
                    .add(txtGoles, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, org.jdesktop.layout.GroupLayout.DEFAULT_SIZE, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(jSeparator3, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE, 10, org.jdesktop.layout.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(org.jdesktop.layout.LayoutStyle.RELATED)
                .add(layout.createParallelGroup(org.jdesktop.layout.GroupLayout.BASELINE)
                    .add(btnAceptar)
                    .add(btnCancelar)))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void btnAceptarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnAceptarActionPerformed
        /*Recojemos los datos de los campos.*/
        if( recojer() ){
            /*Los enviamos al gato.*/
            enviarModelo();
            padre.crearMundo(goles);
            /*Cerramos esta ventana.*/
            dispose();
        }
    }//GEN-LAST:event_btnAceptarActionPerformed

    private void hvshActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_hvshActionPerformed
        txtJugador2.setEnabled(true);
        jugador2.setEnabled(true);
    }//GEN-LAST:event_hvshActionPerformed

    private void btnCancelarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnCancelarActionPerformed
        this.dispose();
    }//GEN-LAST:event_btnCancelarActionPerformed

    private void hvpcActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_hvpcActionPerformed
        txtJugador2.setEnabled(false);
        jugador2.setEnabled(false);
    }//GEN-LAST:event_hvpcActionPerformed

    private void cerrarVentana(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cerrarVentana
        this.dispose();
    }//GEN-LAST:event_cerrarVentana

    
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.ButtonGroup Grupo;
    private javax.swing.JButton btnAceptar;
    private javax.swing.JButton btnCancelar;
    private javax.swing.JRadioButton hvpc;
    private javax.swing.JRadioButton hvsh;
    private javax.swing.JLabel img1;
    private javax.swing.JLabel img2;
    private javax.swing.JLabel img3;
    private javax.swing.JMenu jMenu1;
    private javax.swing.JMenuBar jMenuBar1;
    private javax.swing.JMenuItem jMenuItem1;
    private javax.swing.JSeparator jSeparator1;
    private javax.swing.JSeparator jSeparator2;
    private javax.swing.JSeparator jSeparator3;
    private javax.swing.JLabel jugador1;
    private javax.swing.JLabel jugador2;
    private javax.swing.JLabel opcionDos;
    private javax.swing.JLabel opcionDos1;
    private javax.swing.JLabel opcionUno;
    private javax.swing.JTextField txtGoles;
    private javax.swing.JTextField txtJugador1;
    private javax.swing.JTextField txtJugador2;
    // End of variables declaration//GEN-END:variables
    
}
