PGDMP      7                {         	   DumpDiary    16.0    16.0     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16398 	   DumpDiary    DATABASE     �   CREATE DATABASE "DumpDiary" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
    DROP DATABASE "DumpDiary";
                postgres    false            �            1259    24612    dumps    TABLE     �   CREATE TABLE public.dumps (
    id integer NOT NULL,
    user_id integer,
    date timestamp without time zone,
    shape character varying(20),
    color character varying(20),
    amount character varying(20),
    comments character varying(255)
);
    DROP TABLE public.dumps;
       public         heap    postgres    false            �            1259    24611    dumps_id_seq    SEQUENCE     �   CREATE SEQUENCE public.dumps_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.dumps_id_seq;
       public          postgres    false    218            �           0    0    dumps_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.dumps_id_seq OWNED BY public.dumps.id;
          public          postgres    false    217            �            1259    16400    users    TABLE     �   CREATE TABLE public.users (
    id integer NOT NULL,
    username character varying(20),
    password character varying(20),
    age integer,
    gender character varying(20),
    firstname character varying(30),
    lastname character varying(30)
);
    DROP TABLE public.users;
       public         heap    postgres    false            �            1259    16399    users_id_seq    SEQUENCE     �   CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.users_id_seq;
       public          postgres    false    216            �           0    0    users_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;
          public          postgres    false    215                        2604    24615    dumps id    DEFAULT     d   ALTER TABLE ONLY public.dumps ALTER COLUMN id SET DEFAULT nextval('public.dumps_id_seq'::regclass);
 7   ALTER TABLE public.dumps ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    217    218    218                       2604    16403    users id    DEFAULT     d   ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);
 7   ALTER TABLE public.users ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    216    215    216            �          0    24612    dumps 
   TABLE DATA           R   COPY public.dumps (id, user_id, date, shape, color, amount, comments) FROM stdin;
    public          postgres    false    218   �       �          0    16400    users 
   TABLE DATA           Y   COPY public.users (id, username, password, age, gender, firstname, lastname) FROM stdin;
    public          postgres    false    216          �           0    0    dumps_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.dumps_id_seq', 1, false);
          public          postgres    false    217            �           0    0    users_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.users_id_seq', 1, false);
          public          postgres    false    215            $           2606    24617    dumps dumps_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.dumps
    ADD CONSTRAINT dumps_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.dumps DROP CONSTRAINT dumps_pkey;
       public            postgres    false    218            "           2606    16405    users users_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            postgres    false    216            %           2606    24618    dumps dumps_user_id_fkey    FK CONSTRAINT     w   ALTER TABLE ONLY public.dumps
    ADD CONSTRAINT dumps_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id);
 B   ALTER TABLE ONLY public.dumps DROP CONSTRAINT dumps_user_id_fkey;
       public          postgres    false    216    4642    218            �     x����n�0�����Py��߬�jJP@H�iW�eK	*��}�u��5��4���|g�x� F�����`(���V���E<�h�g�)ă�&�,���)�6F�D]$`�y�2,[ (j*�.P��2�i���!�)���\�<m��o�o�}B#EE}�E��$��:A3O�%��R��Z�|E�87�ˇ#9�����7�9Gg�m'���h0�c���)Ͽ4&�.)�h�y�\X���.NS�� ���cPL:"���r����v��`7��kV���p��4.5Y�G��rs`����Or����!e=���Bd�}E~T����	�p?��:������J���m�I�߾u���E�k�'�+�<}fY�B��[%��t�ږ�[&a�H�7z������5�=�l�r�]3fƒ�=��쪩��q/ׅ��,����P���q�3C�意~Κ(f�Ee�d�I�iRx�Y�¡�����܆>N�9v�le�,8���7a��SdT���Y[d��%�H8M�Ѹ���D��n�Ӷ����?�:�
      �   (   x�3�L/J,�,H,..�/J�4�ts�u�q������� �     